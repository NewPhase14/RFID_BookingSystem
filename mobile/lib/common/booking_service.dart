import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:http/http.dart' as http;
import 'package:mobile/models/availability.dart';
import 'package:mobile/models/bookings.dart';
import 'package:mobile/models/create_booking.dart';
import '../models/login.dart';
import '../models/profile.dart';
import '../models/services.dart';

abstract class BookingService {
  Future<void> login(Login loginData);
  Future<void> logout();
  Future<Profile> getUserByEmail(String email);
  Future<List<Services>> services();
  Future<Services> getServiceById(String id);
  Future<Profile?> getProfileFromStorage();
  Future<List<Bookings>> getTodaysBookingsByUserId(String userId);
  Future<List<Bookings>> getFutureBookingsByUserId(String userId);
  Future<List<Bookings>> getPastBookingsByUserId(String userId);
  Future<List<Availability>> getAvailabilitySlots(String serviceId);
  Future<Bookings> deleteBooking(String bookingId);
  Future<Bookings> createBooking(CreateBooking createBooking);
}

class ApiBookingService extends BookingService {
  final _secureStorage = const FlutterSecureStorage();

  @override
  Future<Profile?> getProfileFromStorage() async {
    final jsonStr = await _secureStorage.read(key: 'profile');
    if (jsonStr == null) return null;
    final json = jsonDecode(jsonStr);
    return Profile.fromJson(json);
  }

  @override
  Future<void> login(Login loginData) async {
    final apiUrl = dotenv.env['API_URL'];
    final response = await http.post(
      Uri.parse('$apiUrl/api/auth/login'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(loginData.toJson()),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      final token = data['jwt'];

      if (token != null) {
        await _secureStorage.write(key: 'jwt', value: token);

        final profile = await getUserByEmail(loginData.email);

        await _secureStorage.write(
          key: 'profile',
          value: jsonEncode(profile.toJson()),
        );
      } else {
        throw Exception('Jwt not found in response');
      }
    } else {
      throw Exception('Login failed: ${response.body}');
    }
  }

  @override
  Future<void> logout() async {
    await _secureStorage.delete(key: 'jwt');
    await _secureStorage.delete(key: 'profile');
  }

  @override
  Future<List<Services>> services() async {
    final apiUrl = dotenv.env['API_URL'];
    final response = await http.get(
      Uri.parse('$apiUrl/api/service/GetAllServices'),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      final List<dynamic> jsonList = jsonDecode(response.body);
      return jsonList.map((json) => Services.fromJson(json)).toList();
    } else {
      throw Exception('Services failed: ${response.body}');
    }
  }

  @override
  Future<Services> getServiceById(String id) async {
    final apiUrl = dotenv.env['API_URL'];
    final response = await http.get(
      Uri.parse('$apiUrl/api/service/GetServiceById?id=$id'),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      final json = jsonDecode(response.body);
      return Services.fromJson(json);
    } else {
      throw Exception('Failed to fetch service by ID: ${response.body}');
    }
  }

  @override
  Future<List<Bookings>> getFutureBookingsByUserId(String userId) async {
    final apiUrl = dotenv.env['API_URL'];
    final response = await http.get(
      Uri.parse('$apiUrl/api/booking/GetFutureBookingsByUserId?userId=$userId'),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      final List<dynamic> jsonList = jsonDecode(response.body);
      return jsonList.map((json) => Bookings.fromJson(json)).toList();
    } else {
      throw Exception(
        'Failed to fetch future bookings by user ID: ${response.body}',
      );
    }
  }

  @override
  Future<List<Bookings>> getPastBookingsByUserId(String userId) async {
    final apiUrl = dotenv.env['API_URL'];
    final response = await http.get(
      Uri.parse('$apiUrl/api/booking/GetPastBookingsByUserId?userId=$userId'),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      final List<dynamic> jsonList = jsonDecode(response.body);
      return jsonList.map((json) => Bookings.fromJson(json)).toList();
    } else {
      throw Exception(
        'Failed to fetch past bookings by user ID: ${response.body}',
      );
    }
  }

  @override
  Future<List<Bookings>> getTodaysBookingsByUserId(String userId) async {
    final apiUrl = dotenv.env['API_URL'];
    final response = await http.get(
      Uri.parse('$apiUrl/api/booking/GetTodaysBookingsByUserId?userId=$userId'),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      final List<dynamic> jsonList = jsonDecode(response.body);
      return jsonList.map((json) => Bookings.fromJson(json)).toList();
    } else {
      throw Exception(
        'Failed to fetch today\'s bookings by user ID: ${response.body}',
      );
    }
  }

  @override
  Future<Profile> getUserByEmail(String email) async {
    final apiUrl = dotenv.env['API_URL'];
    final response = await http.get(
      Uri.parse('$apiUrl/api/user/GetUserByEmail?email=$email'),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      final json = jsonDecode(response.body);
      return Profile.fromJson(json);
    } else {
      throw Exception('Failed to fetch user by email: ${response.body}');
    }
  }

  @override
  Future<List<Availability>> getAvailabilitySlots(String serviceId) async {
    final apiUrl = dotenv.env['API_URL'];
    final response = await http.get(
      Uri.parse(
        '$apiUrl/api/booking/GetAvailabilitySlots?serviceId=$serviceId',
      ),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      final List<dynamic> jsonList = jsonDecode(response.body);
      return jsonList.map((json) => Availability.fromJson(json)).toList();
    } else {
      throw Exception('Failed to fetch availability slots: ${response.body}');
    }
  }

  @override
  Future<Bookings> createBooking(CreateBooking createBooking) async {
    final apiUrl = dotenv.env['API_URL'];

    final token = await _secureStorage.read(key: 'jwt');
    if (token == null) {
      throw Exception('User is not authenticated');
    }

    final response = await http.post(
      Uri.parse('$apiUrl/api/booking/CreateBooking'),
      headers: {'Content-Type': 'application/json', 'Authorization': token},
      body: jsonEncode(createBooking.toJson()),
    );

    if (response.statusCode == 200) {
      final json = jsonDecode(response.body);
      return Bookings.fromJson(json);
    } else {
      throw Exception('Failed to create booking: ${response.body}');
    }
  }

  @override
  Future<Bookings> deleteBooking(String bookingId) async {
    final apiUrl = dotenv.env['API_URL'];

    final token = await _secureStorage.read(key: 'jwt');
    if (token == null) {
      throw Exception('User is not authenticated');
    }

    final response = await http.delete(
      Uri.parse('$apiUrl/api/booking/DeleteBooking?id=$bookingId'),
      headers: {'Authorization': token},
    );

    if (response.statusCode == 200) {
      final json = jsonDecode(response.body);
      return Bookings.fromJson(json);
    } else {
      throw Exception('Failed to delete booking: ${response.body}');
    }
  }
}
