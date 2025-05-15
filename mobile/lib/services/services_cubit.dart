import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/common/booking_service.dart';
import '../models/services.dart';
import 'services_state.dart';

class ServicesCubit extends Cubit<ServicesState> {
  final BookingService service;

  ServicesCubit(this.service) : super(ServicesReady());

  Future<void> loadServices() async {
    emit(ServicesLoading());
    try {
      final serviceList = await service.services();
      emit(ServicesInitialized(serviceList));
    } catch (error) {
      emit(ServicesError(error.toString()));
    }
  }
}
