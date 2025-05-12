import 'dart:convert';
import 'package:http/http.dart' as http;

abstract class BookingService {
  Future<void> login({required String email, required String password});
  Future<void> logout();
}

class ApiBookingService extends BookingService {
  @override
  Future<void> login({required String email, required String password}) async {
    final response = await http.post(
      Uri.parse('/auth/login'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({'email': email, 'password': password}),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      // add token
    } else {
      throw Exception('Login failed: ${response.body}');
    }
  }

  @override
  Future<void> logout() async {
    // token=null
  }
}
