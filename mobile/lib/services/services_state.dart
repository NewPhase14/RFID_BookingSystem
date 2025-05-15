import 'package:flutter/foundation.dart';
import '../models/services.dart';

@immutable
abstract class ServicesState {}

class ServicesReady extends ServicesState {}

class ServicesLoading extends ServicesState {}

class ServicesError extends ServicesState {
  final String message;
  ServicesError(this.message);
}

class ServicesInitialized extends ServicesState {
  final List<Services> services;
  ServicesInitialized(this.services);
}
