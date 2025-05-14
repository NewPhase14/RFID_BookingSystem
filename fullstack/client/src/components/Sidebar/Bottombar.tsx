import React from 'react';
import "../styles.css";

export const Bottombar = () => {
    return (
        <div className="flex sticky top-[calc(100vh_-_48px_-_16px)]
         flex-col h-12 border-t px-2 border-[--color-text-grey] justify-end text-xs">
            <div className="flex items-center justify-between">
                <div>
                    <p className="font-bold">Admin</p>
                    <p className="">Administrator</p>
                </div>
                <button className="px-2 py-1.5 bg-[--color-background-grey] hover:bg-[--color-button-grey] transition-colors rounded">
                    Sign Out
                </button>
            </div>
        </div>
    )
}
