import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/services_detail/service_detail_content.dart';
import 'package:mobile/services_detail/services_detail_cubit.dart';
import 'package:mobile/services_detail/services_detail_state.dart';

class ServiceDetailPage extends StatefulWidget {
  final String serviceId;
  const ServiceDetailPage({super.key, required this.serviceId});

  @override
  State<ServiceDetailPage> createState() => _ServiceDetailPageState();
}

class _ServiceDetailPageState extends State<ServiceDetailPage> {
  int selectedDayIndex = 0;

  void _onDayChanged(int newIndex) {
    setState(() {
      selectedDayIndex = newIndex;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Bookit"),
        leading: BackButton(color: Colors.white),
      ),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 16),
          child: BlocBuilder<ServiceDetailCubit, ServiceDetailState>(
            builder: (context, state) {
              if (state is ServiceDetailLoading) {
                return const Center(child: CircularProgressIndicator());
              } else if (state is ServiceDetailError) {
                return Center(
                  child: Text(
                    state.message,
                    style: const TextStyle(color: Colors.red),
                  ),
                );
              } else if (state is ServiceDetailLoaded) {
                return ServiceDetailContent(
                  service: state.service,
                  selectedDayIndex: selectedDayIndex,
                  onDayChanged: _onDayChanged,
                  serviceId: widget.serviceId,
                );
              }
              return const SizedBox.shrink();
            },
          ),
        ),
      ),
    );
  }
}
