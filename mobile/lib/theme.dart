import 'package:flutter/material.dart';

const Color primaryBlue = Color(0xFF26BBFF);
const Color backgroundDark = Color(0xFF101014);
const Color navbarBackground = Color(0xFF181818);

final ThemeData appTheme = ThemeData(
  brightness: Brightness.dark,

  scaffoldBackgroundColor: backgroundDark,

  // Custom style for input fields (like TextFormField).
  inputDecorationTheme: InputDecorationTheme(
    filled: true,
    fillColor: Colors.white,
    contentPadding: EdgeInsets.symmetric(vertical: 18, horizontal: 16),
    border: OutlineInputBorder(
      borderRadius: BorderRadius.circular(12),
      borderSide: BorderSide.none,
    ),
    hintStyle: TextStyle(color: Colors.black38),
    prefixIconColor: Colors.black87,
  ),

  // Custom styling for ElevatedButtons (raised buttons).
  elevatedButtonTheme: ElevatedButtonThemeData(
    style: ElevatedButton.styleFrom(
      backgroundColor: primaryBlue,
      foregroundColor: Colors.white,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
      padding: EdgeInsets.symmetric(vertical: 14),
      textStyle: TextStyle(fontWeight: FontWeight.bold),
    ),
  ),

  // Custom styling for bottom navigation bar
  bottomNavigationBarTheme: BottomNavigationBarThemeData(
    type: BottomNavigationBarType.fixed,
    backgroundColor: navbarBackground,
    selectedItemColor: primaryBlue,
    unselectedItemColor: Colors.white70,
    showSelectedLabels: true,
    elevation: 8,
  ),

  //Remove ripple/splash effect for navbar
  splashFactory: NoSplash.splashFactory,
  splashColor: Colors.transparent,
  highlightColor: Colors.transparent,
);
