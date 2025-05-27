import 'package:flutter/material.dart';

class ProfileField extends StatelessWidget {
  final String label;
  final String value;

  const ProfileField({super.key, required this.label, required this.value});

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          label,
          style: const TextStyle(color: Colors.white70, fontSize: 14),
        ),
        const SizedBox(height: 6),
        TextField(
          enabled: false,
          decoration: const InputDecoration(
            filled: true,
            fillColor: Colors.black26,
            contentPadding: EdgeInsets.symmetric(vertical: 10, horizontal: 12),
          ),
          controller: TextEditingController(text: value),
          style: const TextStyle(color: Colors.white38),
        ),
      ],
    );
  }
}
