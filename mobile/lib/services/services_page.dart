import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/services/services_cards.dart';
import '../services/services_cubit.dart';
import '../services/services_state.dart';

class ServicesPage extends StatefulWidget {
  const ServicesPage({super.key});

  @override
  State<ServicesPage> createState() => _ServicesPageState();
}

class _ServicesPageState extends State<ServicesPage> {
  @override
  void initState() {
    super.initState();
    context.read<ServicesCubit>().loadAllServices();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Bookit")),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 16),
          child: BlocBuilder<ServicesCubit, ServicesState>(
            builder: (context, state) {
              if (state is ServicesLoading) {
                return const Center(child: CircularProgressIndicator());
              }

              if (state is ServicesError) {
                return Center(child: Text(state.message));
              }

              if (state is ServicesLoaded) {
                final services = state.services;

                if (services.isEmpty) {
                  return const Center(child: Text("No services available."));
                }

                return ListView.builder(
                  itemCount: services.length,
                  itemBuilder: (context, index) {
                    final service = services[index];
                    return Padding(
                      padding: const EdgeInsets.only(bottom: 8),
                      child: ServiceCard(service: service),
                    );
                  },
                );
              }

              return const Center(child: Text("Loading services..."));
            },
          ),
        ),
      ),
    );
  }
}
