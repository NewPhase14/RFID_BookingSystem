import "../styles.css";
import {TopBar} from "./TopBar.tsx";
import {Grid} from "./Grid.tsx";
import useInitializeData from "../../hooks/useInitializeData.tsx";
import LogSelector from "./LogSelector.tsx";

const Dashboard = () => {
    useInitializeData();

    return <div className="bg-background-grey rounded-lg pb-4 shadow">
        <TopBar/>
        <Grid/>
        <LogSelector/>
    </div>


}

export default Dashboard