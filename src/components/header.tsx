import React from 'react';
import styled from '@emotion/styled';
import { Link } from 'gatsby';
import { css } from '@emotion/core';
import { FontWeightProperty } from 'csstype';

const styles = {
    header: css({
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        background: '#eee',
        padding: '0.5rem calc((100vw - 550px - 0.5rem) / 2)',
        borderBottom: '1px solid #ddd',
    }),
    nav: css({
        marginTop: 0,
    }),
};
interface NavLinkProps {
    fontWeight?: FontWeightProperty | FontWeightProperty[];
}

const NavLinkStyles = (props: NavLinkProps) => {
    return css({
        color: '#222',
        fontsize: '1rem',
        margin: '0 0.5rem 0 0',
        textDecoration: 'none',
        fontWeight: props.fontWeight || 'normal',
        '&.current-page': {
            borderBottom: '2px solid #222',
        },

        ' &:last-of-type': {
            marginRight: 0,
        },
    });
};

const NavLink = styled(Link)<NavLinkProps>`
    ${NavLinkStyles}
`;

const Header = () => (
    <header css={styles.header}>
        <NavLink to="/" fontWeight="bold">
            Adventures in Bookland
        </NavLink>
        <nav css={styles.nav}>
            <NavLink to="/" activeClassName="current-page">
                Home
            </NavLink>
            <NavLink to="/about" activeClassName="current-page">
                About
            </NavLink>
        </nav>
    </header>
);

export default Header;
