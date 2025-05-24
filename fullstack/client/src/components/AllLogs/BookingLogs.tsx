
import {useAtom} from "jotai";
import {BookingsAtom} from "../../atoms/atoms.ts";
import "../styles.css"

const BookingLogs = () => {

    const [bookings] = useAtom(BookingsAtom);


    return (

        <div className="overflow-x-auto rounded-box border border-base-content/5 mt-4">
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
                {bookings.map((booking) => (
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