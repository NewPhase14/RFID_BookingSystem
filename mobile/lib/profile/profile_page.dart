import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../common/booking_service.dart';
import 'profile_cubit.dart';
import 'profile_state.dart';
import '../login/login_page.dart';

class ProfilePage extends StatelessWidget {
  const ProfilePage({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create:
          (context) =>
              ProfileCubit(context.read<BookingService>())..loadProfile(),
      child: BlocConsumer<ProfileCubit, ProfileState>(
        listener: (context, state) {
          if (state is ProfileLoggedOut) {
            Navigator.of(
              context,
            ).pushAndRemoveUntil(LoginPage.route(), (route) => false);
          } else if (state is ProfileError) {
            ScaffoldMessenger.of(context).showSnackBar(
              SnackBar(
                content: Text(state.message),
                backgroundColor: Colors.redAccent,
              ),
            );
          }
        },
        builder: (context, state) {
          if (state is ProfileLoading || state is ProfileReady) {
            return const Scaffold(
              body: Center(child: CircularProgressIndicator()),
            );
          } else if (state is ProfileLoaded) {
            final profile = state.profile;

            return Scaffold(
              appBar: AppBar(title: const Text('Bookit')),
              body: SafeArea(
                child: Padding(
                  padding: const EdgeInsets.symmetric(
                    horizontal: 20,
                    vertical: 16,
                  ),
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
                              const Text(
                                'First Name',
                                style: TextStyle(
                                  color: Colors.white70,
                                  fontSize: 14,
                                ),
                              ),
                              const SizedBox(height: 6),
                              TextField(
                                enabled: false,
                                decoration: const InputDecoration(
                                  filled: true,
                                  fillColor: Colors.black26,
                                  contentPadding: EdgeInsets.symmetric(
                                    vertical: 10,
                                    horizontal: 12,
                                  ),
                                ),
                                controller: TextEditingController(
                                  text: profile.firstName,
                                ),
                                style: const TextStyle(color: Colors.white38),
                              ),
                              const SizedBox(height: 16),

                              const Text(
                                'Last Name',
                                style: TextStyle(
                                  color: Colors.white70,
                                  fontSize: 14,
                                ),
                              ),
                              const SizedBox(height: 6),
                              TextField(
                                enabled: false,
                                decoration: const InputDecoration(
                                  filled: true,
                                  fillColor: Colors.black26,
                                  contentPadding: EdgeInsets.symmetric(
                                    vertical: 10,
                                    horizontal: 12,
                                  ),
                                ),
                                controller: TextEditingController(
                                  text: profile.lastName,
                                ),
                                style: const TextStyle(color: Colors.white38),
                              ),
                              const SizedBox(height: 16),

                              const Text(
                                'Email',
                                style: TextStyle(
                                  color: Colors.white70,
                                  fontSize: 14,
                                ),
                              ),
                              const SizedBox(height: 6),
                              TextField(
                                enabled: false,
                                decoration: const InputDecoration(
                                  filled: true,
                                  fillColor: Colors.black26,
                                  contentPadding: EdgeInsets.symmetric(
                                    vertical: 10,
                                    horizontal: 12,
                                  ),
                                ),
                                controller: TextEditingController(
                                  text: profile.email,
                                ),
                                style: const TextStyle(color: Colors.white38),
                              ),
                              const SizedBox(height: 16),

                              const Text(
                                'RFID',
                                style: TextStyle(
                                  color: Colors.white70,
                                  fontSize: 14,
                                ),
                              ),
                              const SizedBox(height: 6),
                              TextField(
                                enabled: false,
                                decoration: const InputDecoration(
                                  filled: true,
                                  fillColor: Colors.black26,
                                  contentPadding: EdgeInsets.symmetric(
                                    vertical: 10,
                                    horizontal: 12,
                                  ),
                                ),
                                controller: TextEditingController(
                                  text: profile.rfid,
                                ),
                                style: const TextStyle(color: Colors.white38),
                              ),
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
              ),
            );
          } else if (state is ProfileError) {
            return Scaffold(
              body: Center(
                child: Text(
                  'Error: ${state.message}',
                  style: const TextStyle(color: Colors.red),
                ),
              ),
            );
          }
          return const SizedBox.shrink();
        },
      ),
    );
  }
}
