class Services {
  final String id;
  final String name;
  final String description;
  final String imageUrl;
  final String publicId;
  final String createdAt;
  final String updatedAt;

  Services({
    required this.id,
    required this.name,
    required this.description,
    required this.imageUrl,
    required this.publicId,
    required this.createdAt,
    required this.updatedAt,
  });

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'description': description,
      'imageUrl': imageUrl,
      'publicId': publicId,
      'createdAt': createdAt,
      'updatedAt': updatedAt,
    };
  }

  factory Services.fromJson(Map<String, dynamic> json) {
    return Services(
      id: json['id'],
      name: json['name'],
      description: json['description'],
      imageUrl: json['imageUrl'],
      publicId: json['publicId'],
      createdAt: json['createdAt'],
      updatedAt: json['updatedAt'],
    );
  }
}
