import 'package:mobile/common/booking_service.dart';
import 'package:mobile/profile/profile_state.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class ProfileCubit extends Cubit<ProfileState> {
  final BookingService bookingService;

  ProfileCubit(this.bookingService) : super(ProfileReady());

  Future<void> loadProfile() async {
    emit(ProfileLoading());
    try {
      final profile = await bookingService.getProfileFromStorage();
      if (profile == null) {
        emit(ProfileError("Profile not found."));
      } else {
        emit(ProfileLoaded(profile));
      }
    } catch (e) {
      emit(ProfileError("Failed to load profile."));
    }
  }

  Future<void> logout() async {
    await bookingService.logout();
    emit(ProfileLoggedOut());
  }
}
