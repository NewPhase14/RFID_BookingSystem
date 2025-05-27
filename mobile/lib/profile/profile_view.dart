import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../profile/profile_cubit.dart';
import '../models/profile.dart';
import 'profile_field.dart';

class ProfileView extends StatelessWidget {
  final Profile profile;

  const ProfileView({super.key, required this.profile});

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 16),
        child: SingleChildScrollView(
          child: Column(
            children: [
              Center(
                child: CircleAvatar(
                  radius: 50,
                  backgroundImage: NetworkImage(
                    'https://api.dicebear.com/9.x/initials/png?seed=${profile.firstName} ${profile.lastName}&scale=100&backgroundColor=039be5',
                  ),
                ),
              ),
              const SizedBox(height: 32),
              Container(
                padding: const EdgeInsets.all(20),
                decoration: BoxDecoration(
                  color: Colors.grey[850],
                  borderRadius: BorderRadius.circular(12),
                ),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    ProfileField(label: 'First Name', value: profile.firstName),
                    const SizedBox(height: 16),
                    ProfileField(label: 'Last Name', value: profile.lastName),
                    const SizedBox(height: 16),
                    ProfileField(label: 'Email', value: profile.email),
                    const SizedBox(height: 16),
                    ProfileField(label: 'RFID', value: profile.rfid),
                  ],
                ),
              ),
              const SizedBox(height: 32),
              SizedBox(
                width: 160,
                height: 48,
                child: ElevatedButton(
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.grey[850],
                    foregroundColor: Colors.orange,
                  ),
                  onPressed: () {
                    context.read<ProfileCubit>().logout();
                  },
                  child: const Text(
                    'Logout',
                    style: TextStyle(fontWeight: FontWeight.bold),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
