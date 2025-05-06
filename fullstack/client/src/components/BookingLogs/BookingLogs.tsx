import {TopBar} from "./TopBar.tsx";
import {Table} from "./Table.tsx";

export const BookingLogs = () => {
    return (
        <div className="bg-background-grey rounded-lg pb-4 shadow">
            <TopBar/>
            <Table/>
        </div>
    )
}
