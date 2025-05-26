import 'package:flutter/material.dart';
import '../models/availability.dart';

class AvailabilitySlotsWidget extends StatelessWidget {
  final Map<String, List<Availability>> slotsByDate;
  final int selectedDayIndex;
  final void Function(int) onDayChanged;
  final void Function(Availability) onSlotSelected;

  const AvailabilitySlotsWidget({
    super.key,
    required this.slotsByDate,
    required this.selectedDayIndex,
    required this.onDayChanged,
    required this.onSlotSelected,
  });

  @override
  Widget build(BuildContext context) {
    final availableDates = slotsByDate.keys.toList()..sort();

    if (availableDates.isEmpty) {
      return const Center(
        child: Text(
          "No available slots.",
          style: TextStyle(color: Colors.white70, fontSize: 16),
        ),
      );
    }

    final dateKey = availableDates[selectedDayIndex];
    final slots = slotsByDate[dateKey]!;

    return Container(
      margin: const EdgeInsets.only(top: 24),
      padding: const EdgeInsets.all(16),
      decoration: BoxDecoration(
        color: Colors.grey[900],
        borderRadius: BorderRadius.circular(16),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              IconButton(
                icon: const Icon(Icons.chevron_left),
                color: selectedDayIndex > 0 ? Colors.white : Colors.white38,
                onPressed:
                    selectedDayIndex > 0
                        ? () => onDayChanged(selectedDayIndex - 1)
                        : null,
              ),
              Text(
                dateKey,
                style: const TextStyle(
                  color: Colors.white,
                  fontSize: 18,
                  fontWeight: FontWeight.bold,
                ),
              ),
              IconButton(
                icon: const Icon(Icons.chevron_right),
                color:
                    selectedDayIndex < availableDates.length - 1
                        ? Colors.white
                        : Colors.white38,
                onPressed:
                    selectedDayIndex < availableDates.length - 1
                        ? () => onDayChanged(selectedDayIndex + 1)
                        : null,
              ),
            ],
          ),
          const SizedBox(height: 12),

          Center(
            child: Wrap(
              spacing: 16,
              runSpacing: 16,
              children:
                  slots.map((slot) {
                    return ElevatedButton(
                      onPressed: () => onSlotSelected(slot),
                      style: ElevatedButton.styleFrom(
                        padding: const EdgeInsets.symmetric(
                          horizontal: 20,
                          vertical: 12,
                        ),
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(12),
                        ),
                      ),
                      child: Text(
                        '${slot.startTime} - ${slot.endTime}',
                        style: const TextStyle(
                          color: Colors.white,
                          fontWeight: FontWeight.w600,
                        ),
                      ),
                    );
                  }).toList(),
            ),
          ),
        ],
      ),
    );
  }
}
