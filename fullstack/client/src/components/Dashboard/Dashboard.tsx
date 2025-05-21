import "../styles.css";
import {TopBar} from "./TopBar.tsx";
import {Grid} from "./Grid.tsx";
import ActivityLogs from "./ActivityLogs.tsx";
import useInitializeData from "../../hooks/useInitializeData.tsx";


const Dashboard = () => {
    useInitializeData();

    return <div className="bg-background-grey rounded-lg pb-4 shadow h-[200vh]">
        <TopBar/>
        <Grid/>
        <ActivityLogs/>
    </div>


}

export default Dashboard