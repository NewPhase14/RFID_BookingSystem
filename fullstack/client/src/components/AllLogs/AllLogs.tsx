import {TopBar} from "./TopBar.tsx";
import SelectOptions from "./SelectOptions.tsx";

export const AllLogs = () => {
    return (
        <div className="bg-background-grey rounded-lg pb-4 shadow">
            <TopBar/>
            <SelectOptions/>
        </div>
    )
}
