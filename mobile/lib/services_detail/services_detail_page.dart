import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/common/booking_service.dart';
import 'package:mobile/services_detail/services_detail_cubit.dart';
import 'package:mobile/services_detail/services_detail_state.dart';
import '../availability/availability_cubit.dart';
import '../availability/availability_slots_widget.dart';
import '../availability/availability_state.dart';

class ServiceDetailPage extends StatefulWidget {
  final String serviceId;

  const ServiceDetailPage({super.key, required this.serviceId});

  @override
  State<ServiceDetailPage> createState() => _ServiceDetailPageState();
}

class _ServiceDetailPageState extends State<ServiceDetailPage> {
  int selectedDayIndex = 0;

  void _onDayChanged(int newIndex) {
    setState(() {
      selectedDayIndex = newIndex;
    });
  }

  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(
      providers: [
        BlocProvider(
          create:
              (_) =>
                  ServiceDetailCubit(ApiBookingService())
                    ..loadServiceById(widget.serviceId),
        ),
        BlocProvider(
          create:
              (_) =>
                  AvailabilityCubit(ApiBookingService())
                    ..loadAvailabilitySlots(widget.serviceId),
        ),
      ],
      child: Scaffold(
        appBar: AppBar(
          title: const Text("Bookit"),
          leading: IconButton(
            icon: const Icon(Icons.arrow_back),
            onPressed: () => Navigator.pop(context),
            color: Colors.white,
          ),
        ),
        body: SafeArea(
          child: BlocBuilder<ServiceDetailCubit, ServiceDetailState>(
            builder: (context, state) {
              if (state is ServiceDetailLoading) {
                return const Center(child: CircularProgressIndicator());
              } else if (state is ServiceDetailError) {
                return Center(
                  child: Text(
                    state.message,
                    style: const TextStyle(color: Colors.red),
                  ),
                );
              } else if (state is ServiceDetailLoaded) {
                final service = state.service;
                return SingleChildScrollView(
                  padding: const EdgeInsets.symmetric(
                    horizontal: 20,
                    vertical: 16,
                  ),
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
                            return const Center(
                              child: CircularProgressIndicator(),
                            );
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
                              onDayChanged: _onDayChanged,
                            );
                          }
                          return const SizedBox.shrink();
                        },
                      ),
                    ],
                  ),
                );
              }
              return const SizedBox.shrink();
            },
          ),
        ),
      ),
    );
  }
}
