import "../styles.css";
import {TopBar} from "./TopBar.tsx";
import {Grid} from "./Grid.tsx";
import ActivityLogs from "./ActivityLogs.tsx";
import useInitializeData from "../../hooks/useInitializeData.tsx";
import BookingLogs from "./BookingLogs.tsx";


const Dashboard = () => {
    useInitializeData();

    return <div className="bg-background-grey rounded-lg pb-4 shadow h-[200vh]">
        <TopBar/>
        <Grid/>
        <ActivityLogs/>
        <BookingLogs/>
    </div>


}

export default Dashboard