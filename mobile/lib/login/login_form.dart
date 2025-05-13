import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_guid/flutter_guid.dart';
import '../models/login.dart';
import 'login_cubit.dart';
import 'login_state.dart';

class LoginForm extends StatefulWidget {
  const LoginForm({super.key});

  @override
  State<LoginForm> createState() => _LoginFormState();
}

class _LoginFormState extends State<LoginForm> {
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    final state = context.watch<LoginCubit>().state;
    final isLoading = state is LoginLoading;

    return Padding(
      padding: const EdgeInsets.all(24),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          // Logo
          Image.asset('assets/bookit-logo.png', height: 80),
          const SizedBox(height: 60),

          // Email Field
          TextField(
            style: TextStyle(color: Colors.black87),
            controller: _emailController,
            decoration: InputDecoration(
              hintText: 'Email',
              prefixIcon: Icon(Icons.person),
            ),
          ),
          const SizedBox(height: 16),

          // Password Field
          TextField(
            style: TextStyle(color: Colors.black87),
            controller: _passwordController,
            decoration: InputDecoration(
              hintText: 'Password',
              prefixIcon: Icon(Icons.lock),
            ),
            obscureText: true,
          ),
          const SizedBox(height: 24),

          // Sign In Button
          SizedBox(
            width: double.infinity,
            child: ElevatedButton(
              onPressed:
                  isLoading
                      ? null
                      : () {
                        final loginData = Login(
                          email: _emailController.text.trim(),
                          password: _passwordController.text,
                          clientId: Guid.newGuid.toString(),
                        );

                        context.read<LoginCubit>().login(loginData);
                      },
              child:
                  isLoading
                      ? CircularProgressIndicator(color: Colors.white)
                      : Text('Sign in'),
            ),
          ),

          const SizedBox(height: 16),
          TextButton(
            onPressed: () {
              // Handle forgot password
            },
            child: Text(
              'Forgot password?',
              style: TextStyle(
                decoration: TextDecoration.underline,
                color: Colors.white70,
              ),
            ),
          ),
        ],
      ),
    );
  }
}
