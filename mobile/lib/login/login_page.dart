import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../bookings/bookings_cubit.dart';
import '../common/navigation.dart';
import '../services/services_cubit.dart';
import 'login_cubit.dart';
import 'login_form.dart';
import 'login_state.dart';
import 'package:mobile/common/booking_service.dart';

class LoginPage extends StatelessWidget {
  const LoginPage({super.key});

  static Route<void> route() {
    return MaterialPageRoute(builder: (context) => const LoginPage());
  }

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => LoginCubit(context.read<BookingService>()),
      child: BlocConsumer<LoginCubit, LoginState>(
        listener: (context, state) {
          if (state is LoggedIn) {
            Navigator.of(context).pushReplacement(
              MaterialPageRoute(
                builder:
                    (_) => MultiBlocProvider(
                      providers: [
                        BlocProvider(
                          create:
                              (context) =>
                                  ServicesCubit(context.read<BookingService>())
                                    ..loadAllServices(),
                        ),
                        BlocProvider(
                          create:
                              (context) =>
                                  BookingCubit(context.read<BookingService>()),
                        ),
                      ],
                      child: const BottomNavBar(),
                    ),
              ),
            );
          } else if (state is LoginError) {
            ScaffoldMessenger.of(context).showSnackBar(
              SnackBar(
                content: Text(state.message),
                backgroundColor: Colors.redAccent,
              ),
            );
          }
        },
        builder:
            (context, state) => const Scaffold(
              body: SafeArea(
                child: Center(
                  child: SingleChildScrollView(
                    padding: EdgeInsets.symmetric(horizontal: 20),
                    child: LoginForm(),
                  ),
                ),
              ),
            ),
      ),
    );
  }
}
