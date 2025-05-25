class Availability {
  final String date;
  final String startTime;
  final String endTime;

  Availability({
    required this.date,
    required this.startTime,
    required this.endTime,
  });

  Map<String, dynamic> toJson() {
    return {'date': date, 'startTime': startTime, 'endTime': endTime};
  }

  factory Availability.fromJson(Map<String, dynamic> json) {
    return Availability(
      date: json['date'],
      startTime: json['startTime'],
      endTime: json['endTime'],
    );
  }
}
