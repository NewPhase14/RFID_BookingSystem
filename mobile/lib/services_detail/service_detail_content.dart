import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:intl/intl.dart';
import 'package:mobile/availability/availability_cubit.dart';
import 'package:mobile/availability/availability_state.dart';
import 'package:mobile/availability/availability_slots_widget.dart';
import 'package:mobile/common/booking_service.dart';
import 'package:mobile/models/availability.dart';
import 'package:mobile/models/create_booking.dart';
import '../../models/services.dart';

class ServiceDetailContent extends StatelessWidget {
  final Services service;
  final int selectedDayIndex;
  final void Function(int) onDayChanged;
  final String serviceId;

  const ServiceDetailContent({
    super.key,
    required this.service,
    required this.selectedDayIndex,
    required this.onDayChanged,
    required this.serviceId,
  });

  Future<void> _onSlotSelected(BuildContext context, Availability slot) async {
    final bookingService = ApiBookingService();

    final profile = await bookingService.getProfileFromStorage();
    if (profile == null) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text('User profile not found')));
      return;
    }

    try {
      final formattedDate = DateFormat(
        'yyyy-MM-dd',
      ).format(DateFormat('dd-MM-yyyy').parse(slot.date));

      final createBooking = CreateBooking(
        userId: profile.id,
        serviceId: serviceId,
        date: formattedDate,
        startTime: slot.startTime,
        endTime: slot.endTime,
      );

      await bookingService.createBooking(createBooking);

      context.read<AvailabilityCubit>().loadAvailabilitySlots(serviceId);
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Booking successful!'),
          backgroundColor: Colors.green,
        ),
      );
    } catch (e) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text('Booking failed: $e')));
    }
  }

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          ClipRRect(
            borderRadius: BorderRadius.circular(16),
            child: Image.network(
              service.imageUrl,
              width: double.infinity,
              height: 220,
              fit: BoxFit.cover,
            ),
          ),
          const SizedBox(height: 24),
          Text(
            service.name,
            style: const TextStyle(
              fontSize: 28,
              fontWeight: FontWeight.bold,
              color: Colors.white,
            ),
          ),
          const SizedBox(height: 12),
          Text(
            service.description,
            style: TextStyle(
              fontSize: 16,
              color: Colors.white.withOpacity(0.7),
              height: 1.5,
            ),
          ),
          const SizedBox(height: 40),
          BlocBuilder<AvailabilityCubit, AvailabilityState>(
            builder: (context, availabilityState) {
              if (availabilityState is AvailabilityLoading) {
                return const Center(child: CircularProgressIndicator());
              } else if (availabilityState is AvailabilityError) {
                return Center(
                  child: Text(
                    availabilityState.message,
                    style: const TextStyle(color: Colors.red),
                  ),
                );
              } else if (availabilityState is AvailabilityLoaded) {
                return AvailabilitySlotsWidget(
                  slotsByDate: availabilityState.slots,
                  selectedDayIndex: selectedDayIndex,
                  onDayChanged: onDayChanged,
                  onSlotSelected: (slot) => _onSlotSelected(context, slot),
                );
              }
              return const SizedBox.shrink();
            },
          ),
        ],
      ),
    );
  }
}
