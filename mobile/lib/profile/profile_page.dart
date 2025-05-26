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
                  child: Column(
                    children: [
                      const SizedBox(height: 40),

                      // Cartoon avatar from DiceBear
                      Center(
                        child: CircleAvatar(
                          radius: 50,
                          backgroundImage: NetworkImage(
                            'https://api.dicebear.com/9.x/initials/png?seed=${profile.firstName}',
                          ),
                        ),
                      ),

                      const SizedBox(height: 40),

                      // Profile info card
                      Container(
                        padding: const EdgeInsets.all(24),
                        decoration: BoxDecoration(
                          color: Colors.grey[900],
                          borderRadius: BorderRadius.circular(16),
                          boxShadow: [
                            BoxShadow(
                              color: Colors.black.withOpacity(0.2),
                              blurRadius: 10,
                              offset: const Offset(0, 5),
                            ),
                          ],
                        ),

                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            TextField(
                              enabled: false,
                              decoration: InputDecoration(
                                fillColor: Colors.grey[850], // grey background
                              ),
                              controller: TextEditingController(
                                text: profile.firstName,
                              ),
                              style: const TextStyle(color: Colors.white54),
                            ),
                            const SizedBox(height: 20),

                            TextField(
                              enabled: false,
                              decoration: InputDecoration(
                                fillColor: Colors.grey[850], // grey background
                              ),
                              controller: TextEditingController(
                                text: profile.lastName,
                              ),
                              style: const TextStyle(color: Colors.white54),
                            ),
                            const SizedBox(height: 20),

                            TextField(
                              enabled: false,
                              decoration: InputDecoration(
                                fillColor: Colors.grey[850], // grey background
                              ),
                              controller: TextEditingController(
                                text: profile.email,
                              ),
                              style: const TextStyle(color: Colors.white54),
                            ),
                            const SizedBox(height: 20),

                            TextField(
                              enabled: false,
                              decoration: InputDecoration(
                                fillColor: Colors.grey[850], // grey background
                              ),
                              controller: TextEditingController(
                                text: profile.rfid,
                              ),
                              style: const TextStyle(color: Colors.white54),
                            ),
                          ],
                        ),
                      ),

                      const SizedBox(height: 40),

                      SizedBox(
                        width: 160,
                        height: 48,
                        child: ElevatedButton(
                          style: ElevatedButton.styleFrom(
                            backgroundColor: Colors.grey[850], // dark grey
                            foregroundColor: Colors.orange, // orange text
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
                      const SizedBox(height: 30),
                    ],
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
