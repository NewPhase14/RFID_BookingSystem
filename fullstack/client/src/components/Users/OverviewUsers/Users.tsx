import {TopBar} from "./TopBar.tsx";
import {Table} from "./Table.tsx";
import useInitializeData from "../../../hooks/useInitializeData.tsx";

export const Users = () => {
    useInitializeData();
    return (
        <div className="bg-background-grey rounded-lg pb-4 shadow">
            <TopBar/>
            <Table/>
        </div>
    )
}
