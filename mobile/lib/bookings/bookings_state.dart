import 'package:flutter/foundation.dart';
import '../models/bookings.dart';

@immutable
abstract class BookingState {}

class BookingsReady extends BookingState {}

class BookingsLoading extends BookingState {}

class BookingsLoaded extends BookingState {
  final List<Bookings> bookings;
  BookingsLoaded(this.bookings);
}

class BookingsError extends BookingState {
  final String message;
  BookingsError(this.message);
}

class BookingActionReady extends BookingState {}

class BookingActionLoading extends BookingState {}

class BookingActionSuccess extends BookingState {
  BookingActionSuccess();
}

class BookingActionError extends BookingState {
  final String message;
  BookingActionError(this.message);
}
