import 'package:flutter/material.dart';

const Color primaryBlue = Color(0xFF26BBFF);
const Color backgroundDark = Color(0xFF101014);
const Color navbarBackground = Color(0xFF181818);

final ThemeData appTheme = ThemeData(
  // Setting the brightness to dark mode.
  brightness: Brightness.dark,

  // Setting the default background color of the Scaffold widget.
  scaffoldBackgroundColor: backgroundDark,

  // Custom style for input fields (like TextFormField).
  inputDecorationTheme: InputDecorationTheme(
    // Fills the input field background with color.
    filled: true,
    fillColor: Colors.white,

    // Sets padding inside the input field.
    contentPadding: EdgeInsets.symmetric(vertical: 18, horizontal: 16),

    // Defines the shape and border of the input field.
    border: OutlineInputBorder(
      borderRadius: BorderRadius.circular(12),
      borderSide: BorderSide.none,
    ),

    // Style for placeholder
    hintStyle: TextStyle(color: Colors.black38),

    // Icon color
    prefixIconColor: Colors.black87,
  ),

  // Custom styling for ElevatedButtons (raised buttons).
  elevatedButtonTheme: ElevatedButtonThemeData(
    style: ElevatedButton.styleFrom(
      // Background color of the button.
      backgroundColor: primaryBlue,

      // Text (foreground) color of the button.
      foregroundColor: Colors.white,

      // Shape of the button (rounded rectangle).
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),

      // Padding inside the button.
      padding: EdgeInsets.symmetric(vertical: 14),

      // Text style inside the button.
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
