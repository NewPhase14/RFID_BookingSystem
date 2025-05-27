import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../bookings/active_booking_card.dart';
import '../bookings/bookings_cubit.dart';
import '../bookings/bookings_state.dart';

class BookingListWidget extends StatelessWidget {
  const BookingListWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocConsumer<BookingCubit, BookingState>(
      listener: (context, state) {
        if (state is BookingActionSuccess) {
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(
              content: Text('Booking cancelled successfully'),
              backgroundColor: Colors.green,
            ),
          );
          context.read<BookingCubit>().loadTodaysBookings();
        } else if (state is BookingActionError) {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(
              content: Text('Failed to cancel booking: ${state.message}'),
              backgroundColor: Colors.redAccent,
            ),
          );
        }
      },
      builder: (context, state) {
        if (state is BookingsLoading || state is BookingActionLoading) {
          return const Center(child: CircularProgressIndicator());
        } else if (state is BookingsError) {
          return Center(child: Text(state.message));
        } else if (state is BookingsLoaded) {
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
                      context.read<BookingCubit>().deleteBooking(booking.id);
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
    );
  }
}
