import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:mobile/bookings/bookings_cubit.dart';
import 'package:mobile/common/booking_service.dart';
import 'package:mobile/profile/profile_cubit.dart';
import 'package:mobile/services/services_cubit.dart';
import 'package:provider/provider.dart';
import 'theme.dart';
import 'login/login_page.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();

  await dotenv.load(fileName: ".env");

  runApp(const BookitApp());
}

class BookitApp extends StatelessWidget {
  const BookitApp({super.key});

  @override
  Widget build(BuildContext context) {
    return Provider<BookingService>(
      create: (_) => ApiBookingService(),
      child: MultiBlocProvider(
        providers: [
          BlocProvider(
            create: (context) => BookingCubit(context.read<BookingService>()),
          ),
          BlocProvider(
            create: (context) => ServicesCubit(context.read<BookingService>()),
          ),
          BlocProvider(
            create: (context) => ProfileCubit(context.read<BookingService>()),
          ),
        ],
        child: MaterialApp(
          title: 'Bookit',
          debugShowCheckedModeBanner: false,
          theme: appTheme,
          home: const LoginPage(),
        ),
      ),
    );
  }
}
