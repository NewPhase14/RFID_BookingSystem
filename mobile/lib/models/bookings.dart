class Bookings {
  final String id;
  final String userId;
  final String serviceName;
  final String email;
  final String date;
  final String startTime;
  final String endTime;
  final String updatedAt;
  final String createdAt;

  Bookings({
    required this.id,
    required this.userId,
    required this.serviceName,
    required this.email,
    required this.date,
    required this.startTime,
    required this.endTime,
    required this.updatedAt,
    required this.createdAt,
  });

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'userId': userId,
      'serviceName': serviceName,
      'email': email,
      'date': date,
      'startTime': startTime,
      'endTime': endTime,
      'updatedAt': updatedAt,
      'createdAt': createdAt,
    };
  }

  factory Bookings.fromJson(Map<String, dynamic> json) {
    return Bookings(
      id: json['id'],
      userId: json['userId'],
      serviceName: json['serviceName'],
      email: json['email'],
      date: json['date'],
      startTime: json['startTime'],
      endTime: json['endTime'],
      updatedAt: json['updatedAt'],
      createdAt: json['createdAt'],
    );
  }
}
