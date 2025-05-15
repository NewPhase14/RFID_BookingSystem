import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:http/http.dart' as http;
import '../models/login.dart';
import '../models/services.dart';

abstract class BookingService {
  Future<void> login(Login loginData);
  Future<void> logout();
  Future<List<Services>> services();
}

class ApiBookingService extends BookingService {
  final _secureStorage = const FlutterSecureStorage();

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
      return jsonList
          .map(
            (json) => Services(
              id: json['id'],
              name: json['name'],
              description: json['description'],
              imageUrl: json['imageUrl'],
              publicId: json['publicId'],
              createdAt: json['createdAt'],
              updatedAt: json['updatedAt'],
            ),
          )
          .toList();
    } else {
      throw Exception('Services failed: ${response.body}');
    }
  }
}
