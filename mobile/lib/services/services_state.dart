import 'package:flutter/foundation.dart';
import '../models/services.dart';

@immutable
abstract class ServicesState {}

class ServicesReady extends ServicesState {}

class ServicesLoading extends ServicesState {}

class ServicesLoaded extends ServicesState {
  final List<Services> services;
  ServicesLoaded(this.services);
}

class ServicesError extends ServicesState {
  final String message;
  ServicesError(this.message);
}
