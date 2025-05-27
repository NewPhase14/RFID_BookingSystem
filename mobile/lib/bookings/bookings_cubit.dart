import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/common/booking_service.dart';
import '../models/create_booking.dart';
import 'bookings_state.dart';

class BookingCubit extends Cubit<BookingState> {
  final BookingService bookingService;

  BookingCubit(this.bookingService) : super(BookingsReady());

  Future<void> loadUpcomingBookings() async {
    emit(BookingsLoading());
    try {
      final profile = await bookingService.getProfileFromStorage();
      if (profile == null) throw Exception('User profile not found');
      final bookings = await bookingService.getFutureBookingsByUserId(
        profile.id,
      );
      emit(BookingsLoaded(bookings));
    } catch (e) {
      emit(BookingsError(e.toString()));
    }
  }

  Future<void> loadPastBookings() async {
    emit(BookingsLoading());
    try {
      final profile = await bookingService.getProfileFromStorage();
      if (profile == null) throw Exception('User profile not found');
      final bookings = await bookingService.getPastBookingsByUserId(profile.id);
      emit(BookingsLoaded(bookings));
    } catch (e) {
      emit(BookingsError(e.toString()));
    }
  }

  Future<void> loadTodaysBookings() async {
    emit(BookingsLoading());
    try {
      final profile = await bookingService.getProfileFromStorage();
      if (profile == null) throw Exception('User profile not found');
      final bookings = await bookingService.getTodaysBookingsByUserId(
        profile.id,
      );
      emit(BookingsLoaded(bookings));
    } catch (e) {
      emit(BookingsError(e.toString()));
    }
  }

  Future<void> createNewBooking(CreateBooking createBooking) async {
    emit(BookingActionLoading());
    try {
      await bookingService.createBooking(createBooking);
      emit(BookingActionSuccess());
    } catch (e) {
      emit(BookingActionError(e.toString()));
    }
  }

  Future<void> deleteBooking(String bookingId) async {
    emit(BookingActionLoading());
    try {
      await bookingService.deleteBooking(bookingId);
      emit(BookingActionSuccess());
    } catch (e) {
      emit(BookingActionError(e.toString()));
    }
  }
}
