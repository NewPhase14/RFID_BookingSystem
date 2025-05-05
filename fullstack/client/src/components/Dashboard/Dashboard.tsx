import "../styles.css";
import {TopBar} from "./TopBar.tsx";
import {Grid} from "./Grid.tsx";
import {Calendar} from "./Calendar.tsx";

const Dashboard = () => {
    return <div className="bg-background-grey rounded-lg pb-4 shadow h-[200vh]">
        <TopBar/>
        <Grid/>
        <Calendar/>
    </div>


}

export default Dashboard