import 'package:flutter/foundation.dart';
import '../models/services.dart';

@immutable
abstract class ServiceDetailState {}

class ServiceDetailReady extends ServiceDetailState {}

class ServiceDetailLoading extends ServiceDetailState {}

class ServiceDetailLoaded extends ServiceDetailState {
  final Services service;
  ServiceDetailLoaded(this.service);
}

class ServiceDetailError extends ServiceDetailState {
  final String message;
  ServiceDetailError(this.message);
}
