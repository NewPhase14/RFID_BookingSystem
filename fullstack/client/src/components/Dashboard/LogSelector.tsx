import { useState } from "react";
import ActivityLogs from "./ActivityLogs";
import BookingLogs from "./BookingLogs";

const LogSelector = () => {
    const [selectedLog, setSelectedLog] = useState("activity");

    return (
        <div className="p-4">
            <div className="mb-4">
                <label htmlFor="log-selector" className="mr-2 font-semibold">
                    Select log type:
                </label>
                <select
                    id="log-selector"
                    value={selectedLog}
                    onChange={(e) => setSelectedLog(e.target.value)}
                    className="rounded-md bg-gray-800 py-2 text-sm focus:outline-none font-semibold hover:text-[--color-text-baby-blue] hover:bg-gray-700"
                >
                    <option value="activity">Activity Logs</option>
                    <option value="bookings">Booking Logs</option>
                </select>
            </div>
            {selectedLog === "activity" && <ActivityLogs />}
            {selectedLog === "bookings" && <BookingLogs />}
        </div>
    );
};

export default LogSelector;
