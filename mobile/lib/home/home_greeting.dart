import 'package:flutter/material.dart';
import '../models/profile.dart';

class GreetingWidget extends StatelessWidget {
  final Profile? profile;
  const GreetingWidget({this.profile, super.key});

  @override
  Widget build(BuildContext context) {
    final userName =
        profile != null ? "${profile!.firstName} ${profile!.lastName}" : "";
    return Column(
      children: [
        const Text(
          "Welcome",
          style: TextStyle(
            fontSize: 26,
            fontWeight: FontWeight.w700,
            color: Colors.white,
          ),
        ),
        const SizedBox(height: 10),
        if (userName.isNotEmpty)
          Padding(
            padding: const EdgeInsets.only(top: 6),
            child: Text(
              userName,
              style: const TextStyle(
                fontSize: 20,
                fontWeight: FontWeight.w400,
                color: Colors.white70,
              ),
            ),
          ),
      ],
    );
  }
}
