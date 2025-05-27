import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/bookings/bookings_cubit.dart';
import 'package:mobile/bookings/bookings_state.dart';
import 'package:mobile/bookings/active_booking_card.dart';
import 'package:mobile/bookings/past_booking_card.dart';

class BookingPage extends StatefulWidget {
  const BookingPage({super.key});

  @override
  State<BookingPage> createState() => _BookingPageState();
}

class _BookingPageState extends State<BookingPage>
    with SingleTickerProviderStateMixin {
  late TabController _tabController;

  @override
  void initState() {
    super.initState();
    _tabController = TabController(length: 2, vsync: this);

    final bookingCubit = context.read<BookingCubit>();

    bookingCubit.loadUpcomingBookings();

    _tabController.addListener(() {
      if (_tabController.indexIsChanging) return;

      if (_tabController.index == 0) {
        bookingCubit.loadUpcomingBookings();
      } else {
        bookingCubit.loadPastBookings();
      }
    });
  }

  @override
  void dispose() {
    _tabController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Bookit'),
        bottom: TabBar(
          controller: _tabController,
          indicatorColor: Colors.white,
          labelColor: Colors.white,
          unselectedLabelColor: Colors.white54,
          labelStyle: const TextStyle(
            fontWeight: FontWeight.bold,
            fontSize: 16,
          ),
          tabs: const [Tab(text: 'Upcoming'), Tab(text: 'Finished')],
        ),
      ),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 16),
          child: BlocConsumer<BookingCubit, BookingState>(
            listener: (context, state) {
              if (state is BookingActionError) {
                ScaffoldMessenger.of(context).showSnackBar(
                  const SnackBar(
                    content: Text('Failed to cancel booking'),
                    backgroundColor: Colors.redAccent,
                  ),
                );
              } else if (state is BookingActionSuccess) {
                ScaffoldMessenger.of(context).showSnackBar(
                  const SnackBar(
                    content: Text('Booking cancelled successfully'),
                    backgroundColor: Colors.green,
                  ),
                );
                if (_tabController.index == 0) {
                  context.read<BookingCubit>().loadUpcomingBookings();
                } else {
                  context.read<BookingCubit>().loadPastBookings();
                }
              }
            },
            builder: (context, state) {
              if (state is BookingsLoading || state is BookingActionLoading) {
                return const Center(child: CircularProgressIndicator());
              } else if (state is BookingsError) {
                return Center(
                  child: Text(
                    state.message,
                    style: const TextStyle(color: Colors.redAccent),
                  ),
                );
              } else if (state is BookingsLoaded) {
                return TabBarView(
                  controller: _tabController,
                  children: [
                    _buildBookingList(state.bookings),
                    _buildBookingList(state.bookings),
                  ],
                );
              }
              return const Center(child: Text('Loading bookings...'));
            },
          ),
        ),
      ),
    );
  }

  Widget _buildBookingList(List bookings) {
    if (bookings.isEmpty) {
      return const Center(
        child: Text('No bookings found.', style: TextStyle(color: Colors.grey)),
      );
    }
    return ListView.builder(
      itemCount: bookings.length,
      itemBuilder: (context, index) {
        final booking = bookings[index];
        return Column(
          children: [
            _tabController.index == 0
                ? ActiveBookingCard(
                  booking: booking,
                  onCancel: () {
                    context.read<BookingCubit>().deleteBooking(booking.id);
                  },
                )
                : PastBookingCard(booking: booking),
            const SizedBox(height: 12),
          ],
        );
      },
    );
  }
}
