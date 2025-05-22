import { useState } from "react";
import ActivityLogs from "./ActivityLogs.tsx";
import BookingLogs from "./BookingLogs.tsx";

const SelectOptions = () => {
    const [selectedLog, setSelectedLog] = useState("activity");

    return (
            <div className="mb-4 px-4">
                <label htmlFor="log-selector" className="mr-2 font-semibold">
                    Select log type:
                </label>
                <select
                    id="log-selector"
                    value={selectedLog}
                    onChange={(e) => setSelectedLog(e.target.value)}
                    className="rounded-md bg-gray-800 py-2 text-sm focus:outline-none font-semibold hover:text-[--color-text-baby-blue] hover:bg-gray-700"
                >
                    <option value="activity">Activity logs</option>
                    <option value="bookings">Booking logs</option>
                </select>
            {selectedLog === "activity" && <ActivityLogs />}
            {selectedLog === "bookings" && <BookingLogs />}
        </div>
    );
};

export default SelectOptions;
