import { css } from '@emotion/core';
import { Link } from 'gatsby';
import { ReactNode } from 'react';

const styles = {
    link: css({
        color: '#222',
        fontsize: '1rem',
        margin: '0 1rem 0 0',
        textDecoration: 'none',
        fontWeight: 'normal',
        '&.current-page': {
            borderBottom: '2px solid #222',
        },

        ' &:last-of-type': {
            marginRight: 0,
        },
    }),
};

interface INavLinkProps {
    to: string;
    children: ReactNode;
}
const NavLink = (props: INavLinkProps) => (
    <Link css={styles.link} to={props.to} activeClassName="current-page">
        {props.children}
    </Link>
);

export default NavLink;
