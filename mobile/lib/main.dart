import 'package:flutter/material.dart';
import 'package:mobile/common/booking_service.dart';
import 'package:provider/provider.dart';
import 'theme.dart';
import 'login/login_page.dart';

void main() {
  runApp(const BookitApp());
}

class BookitApp extends StatelessWidget {
  const BookitApp({super.key});

  @override
  Widget build(BuildContext context) {
    return Provider<BookingService>(
      create: (_) => ApiBookingService(),
      child: MaterialApp(
        title: 'Bookit',
        debugShowCheckedModeBanner: false,
        theme: appTheme,
        home: const LoginPage(),
      ),
    );
  }
}
