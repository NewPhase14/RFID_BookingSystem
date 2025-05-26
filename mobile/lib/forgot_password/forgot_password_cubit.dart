import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/common/booking_service.dart';
import 'forgot_password_state.dart';

class ForgotPasswordCubit extends Cubit<ForgotPasswordState> {
  final BookingService bookingService;

  ForgotPasswordCubit(this.bookingService) : super(ForgotPasswordReady());

  Future<void> sendResetEmail(String email) async {
    emit(ForgotPasswordLoading());
    try {
      await bookingService.forgotPassword(email);
      emit(ForgotPasswordSuccess());
    } catch (e) {
      emit(ForgotPasswordError(e.toString()));
    }
  }
}
