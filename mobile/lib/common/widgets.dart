import 'package:flutter/material.dart';

import '../booking/booking_page.dart';
import '../home/home_page.dart';
import '../profile/profile_page.dart';
import '../room/rooms_page.dart';

class BottomNavBar extends StatefulWidget {
  const BottomNavBar({super.key});

  @override
  BottomNavBarState createState() => BottomNavBarState();
}

/// List of pages that correspond to each item in the BottomNavigationBar.
/// These widgets are displayed in the main content area of the app.
class BottomNavBarState extends State<BottomNavBar> {
  int _currentIndex = 0;
  final List<Widget> _pages = [
    HomePage(),
    RoomsPage(),
    BookingPage(),
    ProfilePage(),
  ];

  /// Callback function to update the selected index when a tab is tapped.
  void onTabTapped(int index) {
    setState(() {
      _currentIndex = index;
    });
  }

  ///scaffold with a body and a bottom navigation bar.
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // Show the page corresponding to the selected index.
      body: _pages[_currentIndex],
      bottomNavigationBar: BottomNavigationBar(
        onTap: onTabTapped, // Called when a tab is tapped.
        currentIndex: _currentIndex, // Highlights the selected tab.
        items: [
          BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Home'),
          BottomNavigationBarItem(
            icon: Icon(Icons.meeting_room),
            label: 'Rooms',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.calendar_month),
            label: 'Bookings',
          ),
          BottomNavigationBarItem(icon: Icon(Icons.person), label: 'Profile'),
        ],
      ),
    );
  }
}
