import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/bookings/bookings_cubit.dart';
import 'package:mobile/bookings/bookings_state.dart';
import 'package:mobile/models/profile.dart';
import 'package:mobile/bookings/active_booking_card.dart';

class HomePage extends StatefulWidget {
  const HomePage({super.key});

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  Profile? profile;

  @override
  void initState() {
    super.initState();
    _loadProfileAndBookings();
  }

  Future<void> _loadProfileAndBookings() async {
    final bookingCubit = context.read<BookingCubit>();
    final storedProfile =
        await bookingCubit.bookingService.getProfileFromStorage();
    setState(() {
      profile = storedProfile;
    });
    await bookingCubit.loadTodaysBookings();
  }

  Widget _buildGreeting() {
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

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Bookit")),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 16),
          child: Column(
            children: [
              const SizedBox(height: 30),
              _buildGreeting(),
              const SizedBox(height: 30),
              const Divider(height: 5, color: Colors.white30),
              const SizedBox(height: 30),
              const Align(
                alignment: Alignment.center,
                child: Text(
                  "Today's Bookings",
                  style: TextStyle(
                    fontSize: 20,
                    fontWeight: FontWeight.w600,
                    color: Colors.white,
                  ),
                ),
              ),
              const SizedBox(height: 16),
              Expanded(
                child: BlocConsumer<BookingCubit, BookingState>(
                  listener: (context, state) {
                    if (state is BookingActionSuccess) {
                      ScaffoldMessenger.of(context).showSnackBar(
                        const SnackBar(
                          content: Text('Booking cancelled successfully'),
                        ),
                      );
                      context.read<BookingCubit>().loadTodaysBookings();
                    } else if (state is BookingActionError) {
                      ScaffoldMessenger.of(
                        context,
                      ).showSnackBar(SnackBar(content: Text(state.message)));
                    }
                  },
                  builder: (context, state) {
                    if (state is BookingsLoading ||
                        state is BookingActionLoading) {
                      return const Center(child: CircularProgressIndicator());
                    }

                    if (state is BookingsError) {
                      return Center(child: Text(state.message));
                    }

                    if (state is BookingsLoaded) {
                      final bookings = state.bookings;

                      if (bookings.isEmpty) {
                        return const Center(
                          child: Text(
                            "No bookings for today.",
                            style: TextStyle(color: Colors.white30),
                          ),
                        );
                      }

                      return ListView.builder(
                        itemCount: bookings.length,
                        itemBuilder: (context, index) {
                          final booking = bookings[index];
                          return Column(
                            children: [
                              ActiveBookingCard(
                                booking: booking,
                                onCancel: () {
                                  context.read<BookingCubit>().deleteBooking(
                                    booking.id,
                                  );
                                },
                              ),
                              const SizedBox(height: 14),
                            ],
                          );
                        },
                      );
                    }

                    return const Center(child: Text("Loading bookings..."));
                  },
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
