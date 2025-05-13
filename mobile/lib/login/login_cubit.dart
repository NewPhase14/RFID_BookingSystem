import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/common/booking_service.dart';
import '../models/login.dart';
import 'login_state.dart';

class LoginCubit extends Cubit<LoginState> {
  final BookingService service;

  LoginCubit(this.service) : super(LoginReady());

  Future<void> login(Login loginData) async {
    emit(LoginLoading());
    try {
      await service.login(loginData);
      emit(LoggedIn());
    } catch (error) {
      final message = error.toString();
      emit(LoginError(message));
    }
  }
}
