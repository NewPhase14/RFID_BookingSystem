import {TopBar} from "./TopBar.tsx";
import {Grid} from "./Grid.tsx";
import useInitializeData from "../../../hooks/useInitializeData.tsx";

export const Services = () => {
    useInitializeData();
    return (
        <div className="bg-background-grey rounded-lg pb-4 shadow">
            <TopBar/>
            <Grid/>
        </div>
    )
}
