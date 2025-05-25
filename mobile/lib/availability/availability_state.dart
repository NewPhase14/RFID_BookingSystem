import 'package:flutter/foundation.dart';
import '../models/availability.dart';

@immutable
abstract class AvailabilityState {}

class AvailabilityInitial extends AvailabilityState {}

class AvailabilityLoading extends AvailabilityState {}

class AvailabilityLoaded extends AvailabilityState {
  final Map<String, List<Availability>> slots;
  AvailabilityLoaded(this.slots);
}

class AvailabilityError extends AvailabilityState {
  final String message;
  AvailabilityError(this.message);
}
