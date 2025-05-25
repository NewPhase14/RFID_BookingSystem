class Login {
  final String email;
  final String password;
  final String clientId;

  Login({required this.email, required this.password, required this.clientId});

  Map<String, dynamic> toJson() {
    return {'email': email, 'password': password, 'clientId': clientId};
  }

  factory Login.fromJson(Map<String, dynamic> json) {
    return Login(
      email: json['email'],
      password: json['password'],
      clientId: json['clientId'],
    );
  }
}
