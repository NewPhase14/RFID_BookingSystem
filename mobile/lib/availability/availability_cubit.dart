import 'package:flutter_bloc/flutter_bloc.dart';
import '../common/booking_service.dart';
import '../models/availability.dart';
import 'availability_state.dart';

class AvailabilityCubit extends Cubit<AvailabilityState> {
  final BookingService service;

  AvailabilityCubit(this.service) : super(AvailabilityInitial());

  Future<void> loadAvailabilitySlots(String serviceId) async {
    emit(AvailabilityLoading());
    try {
      final slots = await service.getAvailabilitySlots(serviceId);
      final grouped = <String, List<Availability>>{};
      for (var slot in slots) {
        grouped.putIfAbsent(slot.date, () => []).add(slot);
      }
      emit(AvailabilityLoaded(grouped));
    } catch (e) {
      emit(AvailabilityError('Failed to load slots: ${e.toString()}'));
    }
  }
}
