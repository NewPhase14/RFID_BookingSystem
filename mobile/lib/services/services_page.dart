import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/services/services_cards.dart';
import '../services/services_cubit.dart';
import '../services/services_state.dart';

class ServicesPage extends StatelessWidget {
  const ServicesPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 16),
          child: BlocBuilder<ServicesCubit, ServicesState>(
            builder: (context, state) {
              if (state is ServicesLoading) {
                return const Center(child: CircularProgressIndicator());
              }

              if (state is ServicesError) {
                return Center(child: Text(state.message));
              }

              if (state is ServicesInitialized) {
                final services = state.services;

                if (services.isEmpty) {
                  return const Center(child: Text("No services available."));
                }

                return ListView.separated(
                  itemCount: services.length,
                  separatorBuilder:
                      (context, index) => const SizedBox(height: 16),
                  itemBuilder: (context, index) {
                    final service = services[index];
                    return ServiceCard(service: service);
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
