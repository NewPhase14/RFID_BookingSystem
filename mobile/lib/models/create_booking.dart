class CreateBooking {
  final String userId;
  final String serviceId;
  final String date;
  final String startTime;
  final String endTime;

  CreateBooking({
    required this.userId,
    required this.serviceId,
    required this.date,
    required this.startTime,
    required this.endTime,
  });

  Map<String, dynamic> toJson() {
    return {
      'userId': userId,
      'serviceId': serviceId,
      'date': date,
      'startTime': startTime,
      'endTime': endTime,
    };
  }

  factory CreateBooking.fromJson(Map<String, dynamic> json) {
    return CreateBooking(
      userId: json['userId'],
      serviceId: json['serviceId'],
      date: json['date'],
      startTime: json['startTime'],
      endTime: json['endTime'],
    );
  }
}
