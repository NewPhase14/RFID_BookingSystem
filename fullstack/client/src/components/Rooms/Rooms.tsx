import {TopBar} from "./TopBar.tsx";
import {Grid} from "./Grid.tsx";

export const Rooms = () => {
    return (
        <div className="bg-background-grey rounded-lg pb-4 shadow">
            <TopBar/>
            <Grid/>
        </div>
    )
}
