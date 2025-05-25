import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/common/booking_service.dart';
import 'package:mobile/services_detail/services_detail_state.dart';

class ServiceDetailCubit extends Cubit<ServiceDetailState> {
  final BookingService bookingService;

  ServiceDetailCubit(this.bookingService) : super(ServiceDetailReady());

  Future<void> loadServiceById(String id) async {
    emit(ServiceDetailLoading());
    try {
      final service = await bookingService.getServiceById(id);
      emit(ServiceDetailLoaded(service));
    } catch (e) {
      emit(ServiceDetailError(e.toString()));
    }
  }
}
