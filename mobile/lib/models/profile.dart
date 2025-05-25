class Profile {
  final String id;
  final String firstName;
  final String lastName;
  final String rfid;
  final String email;
  final String roleName;
  final String createdAt;
  final String updatedAt;

  Profile({
    required this.id,
    required this.firstName,
    required this.lastName,
    required this.rfid,
    required this.email,
    required this.roleName,
    required this.createdAt,
    required this.updatedAt,
  });

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'firstName': firstName,
      'lastName': lastName,
      'rfid': rfid,
      'email': email,
      'roleName': roleName,
      'createdAt': createdAt,
      'updatedAt': updatedAt,
    };
  }

  factory Profile.fromJson(Map<String, dynamic> json) {
    return Profile(
      id: json['id'],
      firstName: json['firstName'],
      lastName: json['lastName'],
      rfid: json['rfid'],
      email: json['email'],
      roleName: json['roleName'],
      createdAt: json['createdAt'],
      updatedAt: json['updatedAt'],
    );
  }
}
