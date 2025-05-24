import {useWsClient} from "ws-request-hook";
import {useEffect} from "react";
import {useAtom} from "jotai";
import {LatestBookingsAtom} from "../../atoms/atoms.ts";
import toast from "react-hot-toast";
import "../styles.css"

const BookingLogs = () => {

    interface Booking {
        eventType: string,
        requestId: string,
        bookings: []
    }

    const [latestBookings, setLatestBookings] = useAtom(LatestBookingsAtom);
    const {onMessage, readyState} = useWsClient();

    useEffect(() => {
        if (readyState != 1) return;
        try {
            const unsubscribe = onMessage<Booking>("BookingsBroadcastDto", (dto) => {
                setLatestBookings(dto.bookings);
                toast.success("Booking logs updated");
            });
            return () => {
                unsubscribe();
            };
        } catch (e) {
            console.error("Error in AllLogs: ", e);
            toast.error("Error in AllLogs: " + e);
        }
    });

    return (

        <div className="overflow-x-auto rounded-box border border-base-content/5  mt-4">
            <table className="table">
                <thead>
                <tr>
                    <th>Service name</th>
                    <th>User email</th>
                    <th>Booking date</th>
                    <th>Booking start</th>
                    <th>Booking end</th>
                    <th>Booking created at</th>
                </tr>
                </thead>
                <tbody>
                {latestBookings.map((booking) => (
                    <tr key={booking.id}>
                        <td>{booking.serviceName}</td>
                        <td>{booking.email}</td>
                        <td>{booking.date}</td>
                        <td>{booking.startTime}</td>
                        <td>{booking.endTime}</td>
                        <td>{booking.createdAt}</td>
                    </tr>
                ))}

                </tbody>
            </table>
        </div>
    )
}


export default BookingLogs;