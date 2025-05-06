import Sidebar from "./Sidebar/Sidebar.tsx";
import Dashboard from "./Dashboard/Dashboard.tsx";
import {BookingLogs} from "./BookingLogs/BookingLogs.tsx";
import {Rooms} from "./Rooms/Rooms.tsx";

const Home = () => {
    return (
        <main className="grid gap-4 p-4 grid-cols-[220px,_1fr]">
        <Sidebar/>
        <BookingLogs/>
        </main>
    )
}

export default Home;