import {useWsClient} from "ws-request-hook";
import {useEffect} from "react";
import {useAtom} from "jotai";
import {LatestActivityLogsAtom, LatestBookingsAtom} from "../../atoms/atoms.ts";
import toast from "react-hot-toast";

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
            onMessage<Booking>("BookingsBroadcastDto", (dto) => {
                setLatestBookings(dto.bookings);
                toast.success("Bookings updated");
            });
        }
        catch (e) {
            console.error("Error in Bookings: ", e);
            toast.error("Error in Bookings: " + e);
        }
    }, [readyState]);

    return (

        <div className="overflow-x-auto">
            <table className="table table-xs">
                <thead>
                <tr>
                    <th>Booking id</th>
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
                        <td>{booking.id}</td>
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